import React, { useState, useEffect } from 'react'
import {
  BrowserRouter as Router, Route, Switch
} from 'react-router-dom'
import { getAll } from './services/polls'
import Poll from './components/Poll'
import { PollInfo } from './components/Poll'
import AddPollForm from './components/AddPollForm'
import './index.css';

const App = () => {
  const [polls, setPolls] = useState([])

  useEffect(() => {
    getAll()
      .then(initialPolls => {
        setPolls(initialPolls)
      })
  }, [])

  const printPolls = () => polls.map(p =>
    <Poll
      key={p.id} poll={p} polls={polls} setPolls={setPolls}
    />)

  return (
    <Router>
      <Switch>
        <Route exact path="/">
          <div>
            <h1>ePoll App</h1>
            <p>by Santeri Kangas</p>
            {printPolls()}
            <br></br>
            <AddPollForm setPolls={setPolls}></AddPollForm>
          </div>
        </Route>
        <Route path="/polls/:id">
          <PollInfo polls={polls}></PollInfo>
        </Route>
      </Switch>
    </Router>
  )
}

export default App;
