namespace Common_Venues
{
    public delegate void StatusWasChangedDelegate();

    public delegate void StatusWasChangedDelegate<in T>(T status);
}