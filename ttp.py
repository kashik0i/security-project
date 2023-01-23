import socket               # Import socket module
import os
import uuid

DES_KEY = None
AES_KEY = None

s = socket.socket()         # Create a socket object
host = socket.gethostname()  # Get local machine name
port = 8081                # Reserve a port for your service.
s.bind((host, port))        # Bind to the port

try:
    s.listen(5)                 # Now wait for client connection.
except KeyboardInterrupt:
    print("TTP Server: Closed")
print("TTP Server: Started")
while True:
    connection = None
    try:
        smsg = b""
        connection, addr = s.accept()     # Establish connection with client.
        print('Got connection from', addr)
        msg = connection.recv(1024)
        if msg == b'AES':
            if AES_KEY == None:
                AES_KEY = os.urandom(16)
                print(AES_KEY)
            smsg = AES_KEY
        if msg == b'DES':
            if DES_KEY == None:
                DES_KEY = os.urandom(4)
                # DES_KEY = uuid.uuid1().int >> 64
                # print(str(DES_KEY))
                # print(DES_KEY)
            smsg = DES_KEY
        print(msg)
        connection.send(smsg)
        connection.close()
    except socket.timeout:
        print("TTP Server: Timeout")
    except ConnectionResetError:
        print("TTP Server: existing connection closed")
    except KeyboardInterrupt:
        if connection:  # <---
            connection.close()
        print("TTP Server: Closed")
        break
