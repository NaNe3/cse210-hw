using System;

partial class Program
{
    static void SeedLibrary(DigitalLibrary library)
    {
        GreekAuthor homer = new GreekAuthor("Homer", -750, -701, "Epic Greek");
        GreekAuthor sophocles = new GreekAuthor("Sophocles", -497, -406, "Attic Greek");
        LatinAuthor virgil = new LatinAuthor("Virgil", -70, -19, "Augustan");
        LatinAuthor cicero = new LatinAuthor("Cicero", -106, -43, "Late Republic");

        LiteraryWork iliad = new EpicPoem("Iliad", 24, "Greek", homer, 24);
        LiteraryWork odyssey = new EpicPoem("Odyssey", 24, "Greek", homer, 24);
        LiteraryWork oedipusRex = new Tragedy("Oedipus Rex", 1, "Greek", sophocles, 5);
        LiteraryWork antigone = new Tragedy("Antigone", 1, "Greek", sophocles, 5);
        LiteraryWork aeneid = new EpicPoem("Aeneid", 12, "Latin", virgil, 12);
        LiteraryWork georgics = new EpicPoem("Georgics", 4, "Latin", virgil, 4);
        LiteraryWork deOratore = new OratoricalWork("De Oratore", 3, "Latin", cicero, "Roman Senate and Statesmen");
        LiteraryWork philippics = new OratoricalWork("Philippics", 14, "Latin", cicero, "Roman Senate");

        library.AddAuthor(homer);
        library.AddAuthor(sophocles);
        library.AddAuthor(virgil);
        library.AddAuthor(cicero);

        library.AddWork(iliad);
        library.AddWork(odyssey);
        library.AddWork(oedipusRex);
        library.AddWork(antigone);
        library.AddWork(aeneid);
        library.AddWork(georgics);
        library.AddWork(deOratore);
        library.AddWork(philippics);
    }
}
