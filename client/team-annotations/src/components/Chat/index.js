import React, {useState, useEffect} from 'react';
import * as signalR from '@aspnet/signalr';

function Chat() {
  const [name, setName] = useState('Anônimo');
  const [message, setMessage] = useState('');
  const [messages, setMessages] = useState([]);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    setName(window.prompt('Seu nome: ', 'Yuri'));
    setHubConnection(new signalR.HubConnectionBuilder().withUrl('http://localhost/TeamAnnotations/chathub').build());
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('sendMessage', (annotation) => {
        const text = `${annotation.name}: ${annotation.text}`;
        setMessages(oldMessages => [...oldMessages, text]);
      });

      hubConnection.start()
                   .then(() => console.log('Connection started!'))
                   .catch(err => console.log('Error while establishing connection :('));
    }
  }, [hubConnection]);

  function sendMessage() {
    var annotation = {
      Name: name,
      Text: message
    };
    hubConnection
      .invoke('SendAnnotation', annotation)
      .catch(err => console.error(err));
  
      setMessage('');      
  };

  return (
    <div className="App">
      <header className="App-header">
        <div>
          <br />
            <input
             type="text"
             value={message}
             onChange={e => setMessage(e.target.value)}
            />

            <button onClick={() => sendMessage()}>Send</button>

            <div>
              {messages.map((messageReceived, index) => (
                <span style={{display: 'block'}} key={index}> {messageReceived} </span>
              ))}
            </div>
        </div>
      </header>
    </div>
  );
}

export default Chat;