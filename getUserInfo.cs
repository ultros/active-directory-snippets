static void Main(string[] args)
{
    getUserInfo();
} 

static void getUserInfo()
{
    String username = "administrator";

    DirectoryEntry myLdapCon = createDirectoryEntry();

    DirectorySearcher searcher = new DirectorySearcher(myLdapCon);
    searcher.Filter = "(cn=" + username + ")";
    Console.WriteLine("Searcher Started");

    SearchResult result = searcher.FindOne();

    if (result != null)
    {
        ResultPropertyCollection fields = result.Properties;

        foreach (String ldapField in fields.PropertyNames)
        {
            foreach (Object myCollection in fields[ldapField])
                Console.WriteLine(String.Format("{0}: {1}",
                    ldapField, myCollection.ToString()));
        }
    }
    Console.WriteLine("Done");
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
