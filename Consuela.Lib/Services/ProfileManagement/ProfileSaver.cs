using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Consuela.Lib.Services.ProfileManagement
{
    /// <summary>
    /// Handles the loading and saving of the <see cref="ProfileManager"/> class which has a
    /// reference to the <seealso cref="Entity.IProfile"/>.
    /// </summary>
    public class ProfileSaver 
        : IProfileSaver
    {
        private const int ThirtyDays = 30;

        private readonly object _fileLock = new object();

        private readonly string _profileFilePath;

        private ProfileManager _profileManager;

        public ProfileSaver()
        {
            //Should this be injected? On the fence about it.
            _profileFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profile.json");
        }

        public ProfileManager Load()
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

                _profileManager = JsonConvert.DeserializeObject<ProfileManager>(json);

                //If the JSON file does not have JSON in it (empty file) then the JSON convert will return null
                //Instantiate the object in advance so that it will be saved as new on this run
                if(_profileManager == null) _profileManager = new ProfileManager();

                //If the profile is null for any reason then initialize it.
                if (_profileManager.Profile == null) _profileManager.Profile = new ProfileWatcher();

                //Any properties that are not set properly will gain defaults. If any changes are found then the file is saved.
                SetDefaultsAsNeeded(_profileManager.Profile);

                _profileManager.RegisterSaveDelegate(SaveHandler);

                return _profileManager;
            }
        }

        public void SaveHandler(object sender, EventArgs e) => Save();

        public void Save()
        {
            lock (_fileLock)
            {
                InternalSave();
            }
        }

        private void InternalSave()
        {
            var json = JsonConvert.SerializeObject(_profileManager, Formatting.Indented);

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

            if (string.IsNullOrWhiteSpace(profile.Logging.Path)) 
            { 
                profile.Logging.Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); 
                
                changed = true; 
            }

            if (profile.Logging.RetentionDays == 0) 
            { 
                profile.Logging.RetentionDays = ThirtyDays;
                
                changed = true;
            }

            if (changed) InternalSave();
        }
    }
}
