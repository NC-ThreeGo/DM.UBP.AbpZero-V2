1、需要增加DbContextInitializerBase基类，然后为不同的数据库类型实现不同的DbContextInitializer；
2、增加配置文件：
	<dbContextInitializer type="TG.UBP.EF.Oracle.OracleDefaultDbContextInitializer, TG.UBP.EF.Oracle" mapperFiles="TG.UBP.Core.EntityConfiguration.Oracle">
	</dbContextInitializer>
3、在DbContextInitializerBase中加载mapperFiles中的映射类；
4、在DbContext.OnModelCreating方法中注册DbContextInitializerBase加载的映射类；
5、在模块Initialize方法中调用initializer初始化EF；