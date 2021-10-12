import axios from 'axios'
const baseUrl = '/polls'

export const getAll = async () => {
    const request = await axios.get(baseUrl)
    const data = request.data.polls
    return data
}

export const getPollOptions = async (id) => {
    const request = await axios.get(`${baseUrl}/${id}`)
    const data = request.data.options
    return data
}

export const vote = async (id, optionId) => {
    const response = await axios.post(`${baseUrl}/${id}/vote/${optionId}`)
    return response.data
}

export const postPoll = async (pollObject) => {
    const response = await axios.post(baseUrl, pollObject)
    return response.data
}