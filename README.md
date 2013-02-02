# GopherSharp

GopherSharp is a minimal library to abstract and simplify accessing gopher holes from .NET/Mono. This makes it even more trivial to create a gopher client by abstracting way socket management and parsing menus, leaving basics like displaying and managing the output of the menu up to you.

## Documentation

This code can be refractored in minor ways at any time. In addition, more functions may be added.

Check out the [gopher RFC](https://tools.ietf.org/html/rfc1436) as well, for best practices.

### Requesting item(s)

To access a menu, use

    List<GopherItem> items = GopherRequester.RequestMenu(hostnameToServer, selectorToMenu); // port is last parameter and option, default is 70

To access a text file or menu as a string, use

    string item = GopherRequester.RequestText(hostnameToServer, selectorToMenu); // port is last parameter and option, default is 70

To access a raw file as a byte array, use

    byte[] item = GopherRequester.RequestRaw(hostnameToServer, selectorToMenu); // port is last parameter and option, default is 70

Query input is not supported.

### GopherItem

A GopherItem abstracts a menu item on a gopher server. Its items include:

 * char ItemType - char representing the type of content the selector references
 * string DisplayString - string representing what the item will be on the menu (don't adjust this in your software)
 * string Selector - contains the location of the linked item
 * string Hostname - contains the server name where the linked item lives
 * int Port - port on which the server listens, normally 70

It has a constructor of either nothing (making an empty information item) or consuming a raw string from a gopher menu item.
