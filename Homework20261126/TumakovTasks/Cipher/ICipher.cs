namespace TumakovTasks;

public interface ICipher
{
    void encode(ref string input);
    void decode(ref string input);
}
