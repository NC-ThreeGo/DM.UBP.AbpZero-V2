基础设施层（Infrastructure）
	当在领域层中为定义了仓储接口，应该在基础设施层中实现这些接口。这里使用EntityFramework。数据库迁移也被用于这一层。

具体规则：
1、基础设施层主要包括以下内容：
	DbContext
	数据库迁移
	......
2、OSharp框架只实现了MSSql、MySql的数据存储，所以增加了对Oracle的支持；
3、由于Oracle官方的EF存在命名大小写和引号等问题，所以采用了Devart的Oracle组件；
4、参照OSharp框架的代码，实现了基于Devart for Oracle的一些基类；


