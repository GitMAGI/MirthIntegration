using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Le informazioni generali relative a un assembly sono controllate dal seguente 
// set di attributi. Per modificare le informazioni associate a un assembly
// occorre quindi modificare i valori di questi attributi.
[assembly: AssemblyTitle("BatchRISPrivati")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BatchRISPrivati")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Se si imposta ComVisible su false, i tipi in questo assembly non saranno visibili 
// ai componenti COM.  Se è necessario accedere a un tipo in questo assembly da 
// COM, impostare su true l'attributo ComVisible per tale tipo.
[assembly: ComVisible(false)]

// Se il progetto viene esposto a COM, il GUID che segue verrà utilizzato per creare l'ID della libreria dei tipi
[assembly: Guid("56a6d8f9-ec71-4faa-8121-27a93ad8bf09")]

// Le informazioni sulla versione di un assembly sono costituite dai seguenti quattro valori:
//
//      Numero di versione principale
//      Numero di versione secondario 
//      Numero build
//      Revisione
//
// È possibile specificare tutti i valori oppure impostare valori predefiniti per i numeri relativi alla revisione e alla build 
// utilizzando l'asterisco (*) come descritto di seguito:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Log4Net configuration
[assembly: log4net.Config.XmlConfigurator(Watch = true)]