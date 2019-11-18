import React, { useState, useEffect } from "react";
import * as signalR from "@aspnet/signalr";
import { Container, MessagesBox } from "./styles";

function Chat() {
  const ENTER_KEY_CODE = 13; 
  const [name, setName] = useState("Anônimo");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    setName(window.prompt("Seu nome: ", "Yuri"));
    setHubConnection(
      new signalR.HubConnectionBuilder()
        .withUrl("http://192.168.17.26/TeamAnnotations/chathub")
        .build()
    );
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on("sendMessage", annotation => {
        const text = `${annotation.name}: ${annotation.text}`;
        setMessages(oldMessages => [...oldMessages, text]);
      });

      hubConnection
        .start()
        .then(() => console.log("Connection started!"))
        .catch(err => console.log("Error while establishing connection :("));
    }
  }, [hubConnection]);

  function handleKeyUp(event) {
    var code = event.keyCode || event.which;
    if (code === ENTER_KEY_CODE && message
      ) {
      sendMessage();
    }
  }

  function sendMessage() {
    var annotation = {
      Name: name,
      Text: message
    };
    hubConnection
      .invoke("SendAnnotation", annotation)
      .catch(err => console.error(err));
    setMessage("");
  }

  return (
    <Container>
      {messages.map((messageReceived, index) => (
        <span style={{ display: "block" }} key={index}>
          {" "}
          {messageReceived}{" "}
        </span>
      ))}

      <MessagesBox>
        <input
          type="text"
          value={message}
          onChange={e => setMessage(e.target.value)}
          onKeyPress={handleKeyUp}
          style={{ width: "100%" }}
        />
        <button
          onClick={() => sendMessage()}
        >
          Send
        </button>
      </MessagesBox>
    </Container>
  );
}

export default Chat;
