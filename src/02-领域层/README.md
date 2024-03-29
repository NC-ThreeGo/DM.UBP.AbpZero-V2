﻿领域层
	领域层就是业务层，是一个项目的核心，所有业务规则都应该在领域层实现。
	实体（Entity）
　　	实体代表业务领域的数据和操作，在实践中，通过用来映射成数据库表。
	仓储（Repository）
　　	仓储用来操作数据库进行数据存取。仓储接口在领域层定义，而仓储的实现类应该写在基础设施层。
	领域服务（Domain service）
　　	当处理的业务规则跨越两个（及以上）实体时，应该写在领域服务方法里面。
	领域事件（Domain Event）
　　	在领域层某些特定情况发生时可以触发领域事件，并且在相应地方捕获并处理它们。
	工作单元（Unit of Work）
　　	工作单元是一种设计模式，用于维护一个由已经被修改(如增加、删除和更新等)的业务对象组成的列表。它负责协调这些业务对象的持久化工作及并发问题。


具体规则：
1、领域层主要包括以下内容：
	实体
	实体映射（由于不同数据库之间存在一定的不兼容性【例如：Oracle不支持GUID的自增长列、支持字段名的长度最大为30】，
				所以要为不同的数据库创建不同的实体映射类且必须为单独的项目，这样才能通过配置文件加载指定的映射信息）
	仓储
	领域服务
	工作单元
	......
2、按模块+功能建立子目录，例如：BaseManage（基础管理）\Authorization（权限管理）；
3、在子目录下创建相应的实体；
4、需要为每种数据库创建不同的实体映射类，且必须为单独项目——TG.UBP.Domain.EntityConfiguration.XXX项目
5、在子目录下创建相应的领域服务类，在其中使用UOW开启事物、修改数据库、提交事物；
6、工作单元是从属于泛型仓储的，也不需要单独创建；


按模块+功能建立子目录
	SysManage（系统管理）\Authorization：权限管理，包括Module、User、Role、Tenant
						  \Identity：身份管理，包括组织、部门、岗位、职务、员工
						  \OAuth
