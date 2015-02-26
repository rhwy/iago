using static System.Console;

namespace Iago
{
  public delegate string Specify();
  public delegate string Description();
  public delegate void TestAction();

  public static class Specs {
    public static void When(string definition, TestAction act) {
      WriteLine("\t[when] "+definition);
      act();
    }
    public static void Then(string definition, TestAction assert) {
      WriteLine("\t[then] "+definition);
      assert();
    }

    public static void And(string definition, TestAction assert){
      WriteLine("\t[.And] "+definition);
    }
  }
}
