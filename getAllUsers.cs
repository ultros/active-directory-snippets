/*
Enumerate Active Directory from a non-domain joined system and list "samaccountname" for each object/user.
*/

static void Main(string[] args)
{
    getAllUsers();
}

static void getAllUsers()
{
    DirectoryEntry myLdapCon = createDirectoryEntry();

    DirectorySearcher searcher = new DirectorySearcher(myLdapCon);
    searcher.Filter = "(&(objectCategory=User))";

    Console.WriteLine("--- Searcher Started ---");
    SearchResultCollection result = searcher.FindAll();

    foreach (SearchResult user in result)
    {
        Console.WriteLine(user.Properties["samaccountname"].OfType<object>().FirstOrDefault());
    }

    Console.WriteLine("--- Completed ---");
    Console.ReadLine();
}

static DirectoryEntry createDirectoryEntry()
{
    //build connection string including active directory username (and password)
    DirectoryEntry ldapConnection = new DirectoryEntry();
    ldapConnection.Username = "john";
    ldapConnection.Password = "P@ssw0rd";
    ldapConnection.Path = "LDAP://dc.test.local/CN=Users,DC=test,DC=local";
    ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

    return ldapConnection;
}
