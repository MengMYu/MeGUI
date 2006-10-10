using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MeGUI.core.plugins.interfaces;

namespace MeGUI.core.details.video
{
    public partial class ProfileControl : UserControl
    {
        public override string Text
        {
            get { return avsProfileLabel.Text; }
            set
            {
                // Adjust the appearance so that the profile combobox fills just the right amount of space
                int width = avsProfileLabel.Width;
                avsProfileLabel.Text = value;
                Point loc = avsProfile.Location;
                loc.X += avsProfileLabel.Width - width;
                avsProfile.Location = loc;
            }
        }

        public ProfileControl()
        {
            InitializeComponent();
        }
        public event EventHandler ConfigClick;
        public event EventHandler ProfileIndexChanged;

        private void avsConfigButton_Click(object sender, EventArgs e)
        {
            if (ConfigClick != null) ConfigClick(sender, e);
        }

        private void avsProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProfileIndexChanged != null) ProfileIndexChanged(sender, e);
        }


    }
    #region helpers
    public struct Empty
    {
        public static readonly InfoGetter<Empty> Getter = delegate { return new Empty(); };
    }

    public class Getter<TSettings> where TSettings : GenericSettings
    {
        public static SettingsGetter<TSettings> FromGettable(Gettable<TSettings> input)
        {
            return new SettingsGetter<TSettings>(delegate { return input.Settings; });
        }
    }

    public class DefaultGetter<TSettings> where TSettings : GenericSettings, new()
    {
        public static SettingsGetter<TSettings> Create()
        {
            return new SettingsGetter<TSettings>(delegate { return new TSettings(); });
        }
    }
    public class DefaultSetter<TSettings> where TSettings : GenericSettings
    {
        public static SettingsSetter<TSettings> Create()
        {
            return new SettingsSetter<TSettings>(delegate(TSettings p) { /*Do nothing */ });
        }
    }
    #endregion
    #region delegates
    public delegate void SelectedProfileChangedEvent(object sender, Profile prof);
    
    public delegate TSettings SettingsGetter<TSettings>() where TSettings : GenericSettings;
    public delegate void SettingsSetter<TSettings>(TSettings _) where TSettings : GenericSettings;
    
    public delegate bool SettingsEditor<TSettings, TInfo>(
        MainForm mainForm, ref TSettings settings, ref string profile, TInfo extra)
        where TSettings : GenericSettings;
    
    public delegate TInfo InfoGetter<TInfo>();
    #endregion

    public class ProfilesControlHandler<TSettings, TInfo>
        where TSettings : GenericSettings
    {
        private string profileType;
        private MainForm mainForm;
        private ProfileControl impl;


        private SettingsGetter<TSettings> GetCurrentSettings;
        private SettingsSetter<TSettings> SetCurrentSettings;
        private SettingsEditor<TSettings, TInfo> EditSettings;
        private InfoGetter<TInfo> GetInfo;
        public event EventHandler ConfigureCompleted;
        public event SelectedProfileChangedEvent ProfileChanged;

        public string SelectedProfile
        {
            get { return impl.avsProfile.SelectedItem.ToString(); }
            set { impl.avsProfile.SelectedItem = value; }
        }

        public void RefreshProfiles()
        {
            impl.avsProfile.Items.Clear();
            foreach (string name in mainForm.Profiles.Profiles(profileType).Keys)
            {
                impl.avsProfile.Items.Add(name);
            }
        }

        private void avsConfigButton_Click(object sender, System.EventArgs e)
        {
            TInfo info = GetInfo();
            TSettings settings = GetCurrentSettings();
            string profile = impl.avsProfile.Text;
            if (EditSettings(mainForm, ref settings, ref profile, info))
            {
                RefreshProfiles();
                int index = impl.avsProfile.Items.IndexOf(profile);
                if (index != -1)
                    impl.avsProfile.SelectedIndex = index;
                else
                    SetCurrentSettings(settings);
            }
            if (ConfigureCompleted != null) ConfigureCompleted(this, null);
        }

        private void avsProfile_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ProfileChanged != null)
            {
                if (impl.avsProfile.SelectedIndex >= 0)
                {
                    Profile prof = this.mainForm.Profiles.Profiles(profileType)[impl.avsProfile.SelectedItem.ToString()];
                    ProfileChanged(this, prof);
                }
                else
                    ProfileChanged(this, null);
            }
        }

        public ProfilesControlHandler(string profileType, MainForm mainForm, ProfileControl p,
            SettingsEditor<TSettings, TInfo> editor, 
            InfoGetter<TInfo> g2, 
            SettingsGetter<TSettings> getter, 
            SettingsSetter<TSettings> setter)
        {
            this.profileType = profileType;
            this.SetCurrentSettings = setter;
            this.GetCurrentSettings = getter;
            this.EditSettings = editor;
            this.GetInfo = g2;
            this.mainForm = mainForm; 
            p.ProfileIndexChanged += new EventHandler(avsProfile_SelectedIndexChanged);
            p.ConfigClick += new EventHandler(avsConfigButton_Click);
            impl = p;
        }
    }
    
    public class MultipleConfigurersHandler<TProfileSettings, TInfo, TCodec, TEncoder>
        where TProfileSettings : GenericSettings
    {
        public MultipleConfigurersHandler(ComboBox impl) { this.impl = impl; }

        public ISettingsProvider<TProfileSettings, TInfo, TCodec, TEncoder> CurrentSettingsProvider
        {
            get
            {
                return (ISettingsProvider<TProfileSettings, TInfo, TCodec, TEncoder>)impl.SelectedItem;
            }
            set
            {
                impl.SelectedItem = value;
            }
        }

        private ComboBox impl;
        public SettingsGetter<TProfileSettings> Getter
        {
            get 
            { 
                return new SettingsGetter<TProfileSettings>(delegate {
                    return CurrentSettingsProvider.GetCurrentSettings();
                });
            }
        }

        public SettingsSetter<TProfileSettings> Setter
        {
            get
            {
                return new SettingsSetter<TProfileSettings>(delegate(TProfileSettings settings)
                {
                    CurrentSettingsProvider.LoadSettings(settings);
                });
            }
        }

        public SettingsEditor<TProfileSettings, TInfo> EditSettings
        {
            get
            {
                return new SettingsEditor<TProfileSettings, TInfo>(
              delegate(MainForm a, ref TProfileSettings b, ref string c, TInfo d)
              {
                  return CurrentSettingsProvider.EditSettings(a, ref b, ref c, d);
              });
            }
        }
    }


}
