namespace ThemeMixer.Themes
{
    public interface IMixable
    {
        bool Load(string packageID);
        void SetCustomValue(object value);
    }
}
