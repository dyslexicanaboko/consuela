using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Consuela.Lib.Services.ProfileManagement
{
    /// <summary>
    /// Handles the loading and saving of the <see cref="ProfileManager"/> class which has a
    /// reference to the <seealso cref="IProfile"/>.
    /// </summary>
    public class ProfileSaver 
        : IProfileSaver
    {
        public delegate void ProfileChanged(object sender, EventArgs e);

        public event ProfileChanged Changed;

        private const int ThirtyDays = 30;

        private readonly object _fileLock = new object();

        private readonly string _profileFilePath;

        private IProfile _profile; //Singleton one to one with what's in the file

        public ProfileSaver()
        {
            //Should this be injected? On the fence about it.
            _profileFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profile.json");
        }

        public void Load()
        {
            lock (_fileLock)
            {
                var json = string.Empty;

                //If the file doesn't exist, just create it
                if (!File.Exists(_profileFilePath))
                {
                    using (File.Create(_profileFilePath)) 
                    { 
                        //Once the file is created, the stream is opened, make sure to close it so the handle is released
                        //The file is going to be empty regardless, so don't bother reading from it at this point.
                    }
                }
                else
                {
                    json = File.ReadAllText(_profileFilePath);
                }

                _profile = JsonConvert.DeserializeObject<Profile>(json);

                //If the JSON file does not have JSON in it (empty file) then the JSON convert will return null
                //Instantiate the object in advance so that it will be saved as new on this run
                if(_profile == null) _profile = new Profile();

                //Any properties that are not set properly will gain defaults. If any changes are found then the file is saved.
                SetDefaultsAsNeeded(_profile);
            }
        }

        public IProfile Get()
        {
            if (_profile == null) Load();
            
            return _profile;
        }

        public void Save(IProfile profileChanges)
        {
            //If the existing profile and the incomingChanges are identical, then do nothing
            if(_profile == profileChanges) return;

            Adopt(profileChanges);
        }

        public void Save()
        {
            lock (_fileLock)
            {
                InternalSave();
            }
        }

        private void InternalSave()
        {
            var json = JsonConvert.SerializeObject(_profile, Formatting.Indented);

            File.WriteAllText(_profileFilePath, json);
        }

        public void SetDefaultsAsNeeded(IProfile profile)
        {
            var changed = false;

            if (profile.Delete.Schedule == null)
            {
                profile.Delete.Schedule = new Schedule();

                changed = true;
            }

            if (profile.Delete.FileAgeThreshold == 0) 
            { 
                profile.Delete.FileAgeThreshold = ThirtyDays;
                
                changed = true; 
            }

            if (string.IsNullOrWhiteSpace(profile.Audit.Path)) 
            { 
                profile.Audit.Path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Audit"); 
                
                changed = true; 
            }

            if (profile.Audit.RetentionDays == 0) 
            { 
                profile.Audit.RetentionDays = ThirtyDays;
                
                changed = true;
            }

            if (changed) InternalSave();
        }

        private void Adopt(IProfile other)
        {
            //Since there are many lists involved, it's just easier to overwrite the whole file
            //Then reload it into memory instead of trying to update each property at a time

            //Set reference to incoming
            _profile = other;

            //Save to disk
            Save();

            //Reload from disk to disassociate from incoming reference
            Load();

            RaiseChangedEvent();
        }

        private void RaiseChangedEvent() => Changed?.Invoke(this, new EventArgs());
    }
}
