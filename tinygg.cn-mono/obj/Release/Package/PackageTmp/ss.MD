#!/bin/bash
#NOTE
#shadowsocks
#Ubuntu 14

apt-get install python2

apt-get install python-pip

pip install shadowsocks

echo "ssserver -d stop" > proxy_stop.sh

echo "ssserver -p 8388 -k fuck -m aes-256-cfb --user nobody -d start" > proxy_start.sh

chmod +x proxy_stop.sh proxy_start.sh

sh proxy_stop.sh

sh proxy_start.sh

##