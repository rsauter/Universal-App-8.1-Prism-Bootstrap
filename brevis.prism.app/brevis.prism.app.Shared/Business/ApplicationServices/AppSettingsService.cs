using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.ApplicationModel;
using Windows.Storage;
using brevis.prism.app.Contracts.ApplicationServices;

namespace brevis.prism.app.Business.ApplicationServices
{
    public class AppSettingsService : IAppSettingsService
    {
        public string AppVersion
        {
            get
            {
                return string.Format("Version: {0}.{1}.{2}.{3}",
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Revision);
            }
        }

        public string SerialNr
        {
            get
            {
                return Get<string>("SerialNr", Guid.NewGuid().ToString());
            }

            set
            {
                Set("SerialNr", value);
            }
        }

        #region Helpers

        private T Get<T>(string key)
        {
            return Get<T>(key, null);
        }

        //private T Get<T>(string key, object defaultValue)
        //{
        //    var localSettings = ApplicationData.Current.RoamingSettings;

        //    var settingObject = localSettings.Values[key];

        //    // type specific default values
        //    if (typeof(T).Name == "Boolean")
        //        defaultValue = false;

        //    if (typeof(T).Name == "String")
        //    {
        //        // check if setting is available
        //        if (settingObject == null || settingObject.ToString() == "")
        //        {
        //            // not yet available: create new key with default value ...
        //            localSettings.Values[key] = defaultValue;

        //            // ... and set to settingObject
        //            settingObject = defaultValue;
        //        }
        //    }

        //    // check if setting is available
        //    if (settingObject == null)
        //    {
        //        // not yet available: create new key with default value ...
        //        localSettings.Values[key] = defaultValue;

        //        // ... and set to settingObject
        //        settingObject = defaultValue;
        //    }

        //    // cast to target type
        //    var ret = (T)Convert.ChangeType(settingObject, typeof(T));

        //    if (ret == null)
        //        throw new NullReferenceException(key);

        //    return ret;
        //}

        private void Set(string key, object value)
        {
            if (value == null)
                throw new NullReferenceException(key);

            var localSettings = ApplicationData.Current.RoamingSettings;
            localSettings.Values[key] = value;
        }

        public T Get<T>(string key, object defaultValue = null)
        {
            try
            {
                /* precondition for defaultvalue:
                 * default value should be same type as estimated return type (T)
                 */
                if (defaultValue != null)
                {
                    defaultValue = (T)Convert.ChangeType(defaultValue, typeof(T));
                }

                // try getting appsetting key - if not found -> log it as warning!
                var roamingSettings = ApplicationData.Current.RoamingSettings;
                object settingObject = roamingSettings.Values[key];
                if (settingObject == null)
                {
                    Debug.WriteLine("AppSettings key '{0}' is missing in app./web.config file!", key);
                }

                // appsetting key not found, no default value -> fatal!
                if (settingObject == null && defaultValue == null)
                {
                    throw new NullReferenceException(
                        string.Format("AppSetting key '{0}' not found and no default value given!", key));
                }

                // appsetting key not found, with default value -> set and use defaultvalue!
                if (settingObject == null && defaultValue != null)
                {
                    Debug.WriteLine("AppSettings key '{0}' was missing! Use default value '{1}'", key, defaultValue);
                    Set(key, defaultValue);
                    settingObject = defaultValue;
                }

                // cast to target type
                var ret = (T)Convert.ChangeType(settingObject, typeof(T));

                if (ret == null)
                    throw new NullReferenceException(key);

                return ret;
            }
            catch (NullReferenceException ceex)
            {
                Debug.WriteLine(ceex.Message);
                throw;
            }
            catch (Exception err)
            {
                Debug.WriteLine(string.Format("Exception in getting AppSettings for key '{0}': {1}", key, err.Message));
                throw;
            }




        }

        #endregion
    }
}
