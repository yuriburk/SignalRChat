import React, {useState, useEffect} from 'react';
import { HubConnection } from 'signalr-client-react';
import './App.css';

function App() {
  const [name, setName] = useState('Anônimo');
  const [message, setMessage] = useState('');
  const [messages, setMessages] = useState([]);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    setName(window.prompt('Seu nome: ', 'Yuri'));
    setHubConnection(new HubConnection('http://localhost:61927/chathub'));
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('sendMessage', (nick, receivedMessage) => {
        const text = `${nick}: ${receivedMessage}`;
        setMessages(messages.concat([text]));
      });

      hubConnection.start()
                   .then(() => console.log('Connection started!'))
                   .catch(err => console.log('Error while establishing connection :('));
    }
  }, [hubConnection]);

  function sendMessage() {
    hubConnection
      .invoke('sendMessage', name, message)
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
              {messages.map((message, index) => (
                <span style={{display: 'block'}} key={index}> {message} </span>
              ))}
            </div>
        </div>
      </header>
    </div>
  );
}

export default App;
