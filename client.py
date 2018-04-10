#!/usr/bin/env python
# -*- coding: utf-8 -*-
import socket
 
target_host = "127.0.0.1"
target_port = 8080
file = open("Assets/Scripts/status.json", "r")
line = file.read()
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect((target_host, target_port))
print(line)
client.send(line.encode('utf-8'))
response = client.recv(4096)
file.close()
client.close()
print(response)
