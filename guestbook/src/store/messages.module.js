import { MessagesService } from '../services/messages.service'

export const messages = {
    namespaced: true,

    state: {},

    mutations: {},

    actions: {
        getMessages({ commit }, options) { // eslint-disable-line no-unused-vars
            return MessagesService.getMessages(options);
        },

        saveMessage({ commit }, message) { // eslint-disable-line no-unused-vars
            return MessagesService.saveMessage(message);
        }
    }
}