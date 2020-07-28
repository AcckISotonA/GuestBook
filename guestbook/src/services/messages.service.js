import config from "./config.json"
const API_URL = config.apiUrl + '/messages';

import axios from 'axios';


export const MessagesService = {
    getMessages(options) {
        return axios.get(API_URL + '/getmessages', { params: options })
            .then(result => {
                return Promise.resolve(result);
            })
            .catch(error => {
                return Promise.reject(error);
            });
    },

    saveMessage(message) {
        return axios.put(API_URL + '/savemessage', message)
    }
}