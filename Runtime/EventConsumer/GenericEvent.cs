namespace Juce.CoreUnity.Events
{
    public delegate void GenericEvent<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}
