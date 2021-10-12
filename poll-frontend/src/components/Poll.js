import React, { useState, useEffect } from 'react'
import {
    useParams, withRouter, Link
} from 'react-router-dom'
import { getPollOptions, vote } from '../services/polls'
import '../index.css';

const Poll = ({ poll }) => {
    return (
        <>
            <Link to={`/polls/${poll.id}`}><h2>{poll.id} - {poll.title} </h2></Link>
        </>
    )
}

const PollDetails = ({ option, pollId, setPollOptions }) => {

    const plusVote = () => {
        const optionId = option.id

        vote(pollId, optionId)
            .then(() => getPollOptions(pollId)
                .then(o => {
                    setPollOptions(o)
                })
            )
    }

    return (
        <>
            <h2>{option.title} - Votes: {option.votes} </h2>
            <Button handleClick={() => plusVote()} />
        </>
    )
}

const Button = ({ handleClick }) => {
    return (
        <button onClick={handleClick}>
            Vote
        </button>
    )
}

const PollInfos = ({polls}) => {
    const [pollOptions, setPollOptions] = useState([])
    const pollId = useParams().id
    const pollTitle = polls[pollId-1].title

    useEffect(() => {
        getPollOptions(pollId)
            .then(o => {
                setPollOptions(o)
            })
    }, [pollId])

    const printDetails = () => pollOptions.map(o =>
        <PollDetails key={o.id} option={o} pollId={pollId} setPollOptions={setPollOptions} />)

    if (!pollOptions) {
        return null
    }
    return (
        <>
        <h1>{pollTitle}</h1>
        <br></br>
            {printDetails()}
        </>
    )
}

export const PollInfo = withRouter(PollInfos)
export default withRouter(Poll)