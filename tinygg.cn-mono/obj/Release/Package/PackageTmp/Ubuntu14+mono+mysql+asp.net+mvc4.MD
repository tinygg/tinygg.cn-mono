yum install bison make gcc gcc-c++ libstdc++-devel gettext pkgconfig glib2-devel
---------------------------------------------------------------------
apt-get update
apt-get upgrade

apt-get install apache2
/etc/init.d/apache2 stop

apt-get install mono-apache-server libapache2-mod-mono

---------------
Setting up libapache2-mod-mono (2.11+git20130708.6b73e85-4ubuntu1) ...
Using mono-apache-server4...
apache2_invoke: Enable module mod_mono_auto
 * Restarting web server apache2
---------------

## 站点配置文件 也是成功的关键

<VirtualHost *:80>
  ServerName tinygg.cn
  ServerAdmin admin@tinygg.cn
  ServerAlias www.tinygg.cn
  DocumentRoot /var/www/tinygg.cn/html
  ErrorLog /var/www/tinygg.cn/log/error.log
  CustomLog /var/www/tinygg.cn/log/access.log combined

  MonoServerPath tinygg.cn "/usr/bin/mod-mono-server4"
  MonoDebug tinygg.cn true
  MonoSetEnv tinygg.cn MONO_IOMAP=all
  MonoApplications tinygg.cn "/:/var/www/tinygg.cn/html"
  <Location "/">
    Allow from all
    Order allow,deny
    MonoSetServerAlias tinygg.cn
    SetHandler mono
    SetOutputFilter DEFLATE
    SetEnvIfNoCase Request_URI "\.(?:gif|jpe?g|png)$" no-gzip dont-vary
  </Location>
  <IfModule mod_deflate.c>
    AddOutputFilterByType DEFLATE text/html text/plain text/xml text/javascript
  </IfModule>
</VirtualHost>

##setting site tinygg.cn
mkdir /var/www/tinygg.cn/html
mkdir /var/www/tinygg.cn/log


##test config
/usr/local/apache2/bin/apachectl configtest

## restart apache
/etc/init.d/apache2 start 

## transport site files, compiled by visual studio 2010
rz mono-blog.zip

#install mysql
apt-get install mariadb-server-5.5

## mysql设置外网访问
netstat -an|grep 3306
3306端口显示的是：127.0.0.1表示的是本地
打开mysql配置文件sudo vim /etc/mysql/mysql.conf.d/mysqld.cnf
将bind-address = 127.0.0.1注销?,非常重要！！！

GRANT ALL PRIVILEGES ON *.* TO 'root'@'%'IDENTIFIED BY 'root' WITH GRANT OPTION;
flush privileges;

再次执行 netstat -an|grep 3306

## mysql console插入中文乱码的问题

vim /etc/mysql/my.cnf 。(5.5以后系统)如下修改：
[client]
default-character-set=utf8
[mysqld]
default-storage-engine=INNODB
character-set-server=utf8
collation-server=utf8_general_ci