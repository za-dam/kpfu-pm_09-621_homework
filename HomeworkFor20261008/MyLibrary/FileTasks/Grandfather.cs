namespace MyLibrary.FileTasks;

public struct Grandfather
{
    public string Name;
    public GrumpinessLevel Grumpiness;
    public string[] GrumblePhrases;
    public int BruisesCount;

    public Grandfather(string name, GrumpinessLevel grumpiness, string[] phrases)
    {
        Name = name;
        Grumpiness = grumpiness;
        GrumblePhrases = phrases;
        BruisesCount = 0;
    }

    public static int CheckSwearWords(Grandfather grandfather, params string[] badWords)
    {
        int newBruises = 0;

        if (grandfather.GrumblePhrases == null || badWords == null) return 0;

        foreach (string phrase in grandfather.GrumblePhrases)
        {
            string lowerPhrase = phrase.ToLower();

            foreach (string badWord in badWords)
            {
                if (lowerPhrase.Contains(badWord.ToLower()))
                {
                    newBruises++;
                }
            }
        }

        return newBruises;  
    }
}
