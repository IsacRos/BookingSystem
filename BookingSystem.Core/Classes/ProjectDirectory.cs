namespace BookingSystem.Core.Classes;

public class ProjectDirectory
{
    private string binDirectory = AppDomain.CurrentDomain.BaseDirectory;
    string projectDirectory;
    public ProjectDirectory()
    {
        projectDirectory = Directory.GetParent(Directory.GetParent(binDirectory)!.FullName)!.FullName;
    }
    public string GetProjectDirectory() => projectDirectory;
};
