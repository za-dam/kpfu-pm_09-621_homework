using TumakovTasks;

public class CheckInterfaceWithAs
{
    public static List<object[]> GetTestData()
    {
        var list = new List<object[]>();

        list.Add(new object[] { 42, true });
        list.Add(new object[] { 3.14, true });
        list.Add(new object[] { DateTime.Now, true });
        list.Add(new object[] { "hello", false });
        list.Add(new object[] { null, false });
        list.Add(new object[] { new int[] { 1, 2 }, false });
        list.Add(new object[] { new object(), false });

        return list;
    }


    [Theory]
    [MemberData(nameof(GetTestData))]
    public void CheckInterfaceWithAs_WhenCalled_ReturnsExpectedBool(object parameter, bool expected)
    {
        bool result = Utilites.CheckInterfaceWithAs(parameter);

        Assert.Equal(expected, result);
    }
}