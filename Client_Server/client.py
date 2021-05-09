import zmq
import sys

# ZeroMQ Context
context = zmq.Context()

# Define the socket using the "Context"
sock = context.socket(zmq.REQ)
sock.connect("tcp://127.0.0.1:5577")

while True:
    inp = input("What message you want to send? : ")
    # Send a "message" using the socket
    # sock.send_string(str(sys.argv[1:]))
    sock.send_string(str(inp))
    print(sock.recv().decode("utf-8"))
    inp = input("Do you want to make another request? Press y to continue and n for exit : ")
    if (inp == 'n' or inp == 'N'):
        break