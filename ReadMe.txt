1) In order to support simultaneous connections of 3 and more users, this application has to be deployed to IIS Express.

"Supported server IIS versions

When SignalR is hosted in IIS, the following versions are supported. Note that if a client operating system is used, such as for development (Windows 8 or Windows 7), 
full versions of IIS or Cassini should not be used, since there will be a limit of 10 simultaneous connections imposed, 
which will be reached very quickly since connections are transient, frequently re-established, and are not disposed immediately upon no longer being used. IIS Express should be used on client operating systems."

http://www.asp.net/signalr/overview/getting-started/supported-platforms

2) Known issue:
SignalR doesn`t support sharing one connection (user account) across multiple tabs/browsers out of the box. 
It requires additional research and effort to implement this feature and seems this is out of the scope of the current task.