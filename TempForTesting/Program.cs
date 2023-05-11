// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

ManagedComputer mc = new ManagedComputer();

foreach (ServerInstance si in mc.ServerInstances)
{
    Console.WriteLine("The installed instance name is " + si.Name);
}