import zmq

# ZeroMQ Context
context = zmq.Context()

# Define the socket using the "Context"
sock = context.socket(zmq.REP)
sock.bind("tcp://127.0.0.1:5577")

# Run a simple "Echo" server
while True:
    message = sock.recv().decode("utf-8")
    sock.send_string(str(message))
    print( "Echo: " + str(message))