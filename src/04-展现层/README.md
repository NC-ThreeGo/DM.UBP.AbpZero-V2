WEB与展现层（Web & Presentation）

Web层使用ASP.NET MVC和Web API来实现。可分别用于多页面应用程序(MPA)和单页面应用程序(SPA)。
在SPA中，所有资源被一次加载到客户端浏览器中（或者先只加载核心资源，其他资源懒加载），然后通过AJAX调用服务端WebApi接口获取数据，再根据数据生成HTML代码。不会整个页面刷新。现在已经有很多SPA的JS框架，例如： AngularJs、 DurandalJs、BackboneJs、EmberJs。 ABP可以使用任何类似的前端框架，但是ABP提供了一些帮助类，让我们更方便地使用AngularJs和DurandalJs。

在经典的多页面应用（MPA）中，客户端向服务器端发出请求，服务器端代码（ASP.NET MVC控制器）从数据库获得数据，并且使用Razor视图生成HTML。这些被生成后的HTML页面被发送回客户端显示。每显示一个新的页面都会整页刷新。

SPA和MPA涉及到完全不同的体系结构，也有不同的应用场景。一个管理后台适合用SPA，博客就更适合用MPA，因为它更利于被搜索引擎抓取。

SignalR是一种从服务器到客户端发送推送通知的完美工具。它能给用户提供丰富的实时的体验。

已经有很多客户端的Javascript框架或库，JQuery是其中最流行的，并且它有成千上万免费的插件。使用Bootstrap可以让我们更轻松地完成写Html和CSS的工作。

ABP也实现了根据Web API接口自动创建 Javascript的代码函数，来简化JS对Web Api的调用。还有把服务器端的菜单、语言、设置等生成到JS端。（但是在我自己的项目中，我是把这些自动生成功能关闭的，因为必要性不是很大，而这些又会比较影响性能）。

ABP会自动处理服务器端返回的异常，并以友好的界面提示用户。
