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
                var json = File.ReadAllText(_profileFilePath);

                _profileManager = JsonConvert.DeserializeObject<ProfileManager>(json);
                _profileManager.RegisterSaveDelegate(SaveHandler);

                SetDefaultsAsNeeded(_profileManager.Profile);

                return _profileManager;
            }
        }

        public void SaveHandler(object sender, EventArgs e) => Save();

        public void Save()
        {
            lock (_fileLock)
            {
                var json = JsonConvert.SerializeObject(_profileManager, Formatting.Indented);

                File.WriteAllText(_profileFilePath, json);
            }
        }

        public void SetDefaultsAsNeeded(IProfile profile)
        {
            if (profile.Delete.Schedule == null) profile.Delete.Schedule = new Schedule();
            
            if (profile.Delete.FileAgeThreshold == 0) profile.Delete.FileAgeThreshold = ThirtyDays;

            if (string.IsNullOrWhiteSpace(profile.Logging.Path)) profile.Logging.Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if(profile.Logging.RetentionDays == 0) profile.Logging.RetentionDays = ThirtyDays;
        }
    }
}
