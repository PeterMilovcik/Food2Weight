using Xamarin.Essentials;

namespace Food2Weight.Services.Preference
{
    public class PreferenceService : IPreferenceService
    {
        public double GoalWeight
        {
            get => Preferences.Get("GoalWeight", 0.0);
            set => Preferences.Set("GoalWeight", value);
        }
    }
}