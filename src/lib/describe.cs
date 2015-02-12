namespace NDescribe
{
  public delegate string Specify(object keyword);
  public delegate void TestAction();

  public static class Specs {
    public static void When(string definition, TestAction act) {
      act();
    }
    public static void Then(string definition, TestAction assert) {
      assert();
    }
  }
}
