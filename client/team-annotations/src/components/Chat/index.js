import React, { useState, useEffect, useRef } from "react";
import { Container, MessagesBox } from "./styles";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import * as signalR from "@aspnet/signalr";

import axios from "axios";

const Chat = () => {
  const messagesBox = useRef(null);
  const ENTER_KEY_CODE = 13;
  const [name, setName] = useState("Anônimo");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    var userName = window.prompt("Seu nome: ", "Yuri");
    axios.get(`${window.ENV.API}messages?limit=50`).then((response) => {
      setName(userName);
      setMessages(response.data);
      response.data.map((m) => {
        m.color = "gray";
        m.date = getFormatedDate(m.date);
        return m;
      });

      setHubConnection(
        new signalR.HubConnectionBuilder()
          .withUrl(`${window.ENV.CHATHUB}?username=${userName}`)
          .build()
      );
    });
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection
        .start()
        .then(() => console.log("Conectou com sucesso no hub."))
        .catch((err) => console.log("Erro ao conectar no hub."));

      hubConnection.on("sendMessage", (messageReceived) => {
        var isUser = messageReceived.name === name;
        messageReceived.color = isUser ? "green" : "blue";
        messageReceived.date = getFormatedDate(messageReceived.date);
        setMessages((oldMessages) => [...oldMessages, messageReceived]);
        console.log("mensagem");
        messagesBox.current.scrollTop = messagesBox.current.scrollHeight;

        if (!isUser) {
          new Notification(messageReceived.name, {
            body: messageReceived.text,
          });
        }
      });
    }
  }, [hubConnection, name]);

  function handleKeyUp(event) {
    var code = event.keyCode || event.which;
    if (code === ENTER_KEY_CODE && !event.shiftKey && message) {
      sendMessage(name, message);
    }
  }

  function sendMessage(name, message) {
    if (!message || !message.replace(/\s/g, "").length) {
      return;
    }

    hubConnection
      .invoke("SendMessage", { Name: name, Text: message })
      .catch((err) => console.error(err));
    setMessage("");
  }

  function getFormatedDate(date) {
    date = new Date(date);
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var hour = date.getHours();
    var minute = date.getMinutes();

    return `${day}/${
      month < 10 ? `0${month}` : `${month}`
    }/${year} ${hour}:${minute}`;
  }

  return (
    <Container>
      <div
        ref={messagesBox}
        style={{
          margin: "0px 0px 5px 5px",
          overflowY: "auto",
          flex: 1,
        }}
      >
        <ul style={{ padding: 0, listStyle: "none" }}>
          {messages.map((messageReceived, index) => (
            <li key={index}>
              <span style={{ color: messageReceived.color }}>
                {messageReceived.name}{" "}
              </span>
              <span style={{ color: "gray" }}>({messageReceived.date}): </span>
              <span style={{ wordWrap: "break-word" }}>
                {messageReceived.text}
              </span>
            </li>
          ))}
        </ul>
      </div>

      <MessagesBox>
        <TextField
          id="filled-basic"
          label="Mensagem"
          variant="filled"
          onChange={(e) => setMessage(e.target.value)}
          value={message}
          onKeyPress={handleKeyUp}
          style={{
            width: "100%",
            backgroundColor: "white",
          }}
        />
        <Button
          variant="contained"
          color="primary"
          onClick={() => sendMessage(name, message)}
          style={{
            boxShadow: "none",
            color: "#fff",
            width: "100px",
            borderRadius: "0%",
            height: "100%",
          }}
        >
          Enviar
        </Button>
      </MessagesBox>
    </Container>
  );
};

export default Chat;
