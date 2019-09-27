namespace Iago.Language
{
    public delegate string Specify();
    public delegate string Description();
    public delegate void DefineAction();
    public delegate void CheckAction();
    public delegate void CheckActionWithSamples<T>(T values);
}