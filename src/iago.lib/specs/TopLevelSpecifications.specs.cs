namespace Iago.TopLevelSpecifications
{
  using Iago;

  public class SpecificationsSpecs
  {
    public SpecificationsSpecs()
    {
      Specify that = ()=>
        "By a defaultConvention, Specification classes end with [spec]";

      Description help = ()=>
        @"At start, specification loaders are registred, then each loader
        will find the specification classes. The default loader will scan
        running assemblies and search for classes that end with [spec]";


    }
  }
}
