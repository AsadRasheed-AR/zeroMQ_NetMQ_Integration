import time
import zmq
import random

def consumer():
    context = zmq.Context()
    # recieve work
    consumer_receiver = context.socket(zmq.PULL)
    consumer_receiver.bind("tcp://127.0.0.1:5557")
    # send work
    consumer_sender = context.socket(zmq.PUSH)
    consumer_sender.connect("tcp://127.0.0.1:5558")
    
    while True:
        print("waiting for message on port 5558")
        print(consumer_receiver.recv().decode("utf-8"))
        inp = input("What message you want to send? : ")
        consumer_sender.send_string(str(inp))
        inp = input("Do you want to make another request? Press y to continue and n for exit : ")
        if (inp == 'n' or inp == 'N'):
            break

consumer()