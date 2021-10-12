import React, { useState } from 'react'
import { getAll, postPoll } from '../services/polls'
import '../index.css';

const AddPollForm = ({ setPolls }) => {

    const [option, setOption] = useState([])
    const [allOptions, setAllOptions] = useState([])
    const [title, setTitle] = useState([])

    const addOption = (option, e) => {
        e.preventDefault()
        setAllOptions(allOptions.concat(option))
        setOption('')
    }

    const createPoll = (e) => {
        e.preventDefault()

        if (title.length === 0) {
            setTitle('')
        }

        const pollObject = {
            title: title,
            options: allOptions
        }
        setTitle('')
        setAllOptions([])
        postPoll(pollObject)
            .then(() => getAll()
                .then(polls => {
                    setPolls(polls)
                }))
    }



    const printOptions = () => {
        return (
            <>
                <h3>Options added: {allOptions.length}</h3>
                <ul>
                    {allOptions.map(o =>
                        <li key={Math.floor(Math.random() * 100)}>{o}</li>)}
                </ul>
            </>
        )
    }

    return (
        <div>
            <h2>Create new Poll</h2>
            {printOptions()}
            <form onSubmit={createPoll}>
                Title:<input type="text" value={title} onChange={e => setTitle(e.target.value)} required /> <br />
                Option:<input type="text" value={option} onChange={e => setOption(e.target.value)} />
                <button type="submit" onClick={(e) => addOption(option, e)}>Add option to Poll</button> <br />
                <button type="submit">Create Poll</button>
            </form>
        </div>
    )
}

export default AddPollForm;