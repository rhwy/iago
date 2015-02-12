using static System.Console;

namespace NDescribe
{
  public delegate string Specify();
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
  }
}
