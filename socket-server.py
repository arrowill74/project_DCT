print('server start...')
if __name__ == '__main__':  
    import socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.bind(('127.0.0.1', 8001))  
    sock.listen(5)
    while True:  
        connection,address = sock.accept()
        try:
            connection.settimeout(5)
            buf = connection.recv(1024)
            print(buf.decode("utf-8"))
            s = "hi"
            connection.send(s.encode('utf-8'))
        except socket.timeout:
            print('time out')
        connection.close()
