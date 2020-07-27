<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="8" xl="4">
        <v-card dark class="elevation-12">
          <v-toolbar color="primary" flat>
            <v-toolbar-title>Добавление сообщения</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form ref="form">
              <v-text-field
                v-model="userName"
                label="User Name "
                prepend-icon="mdi-account"
                type="text"
                :rules="userNameRules"
                autofocus
                :tabindex="1"
              ></v-text-field>
              <v-text-field
                v-model="email"
                label="E-mail"
                prepend-icon="mdi-email"
                type="text"
                :rules="emailRules"
              ></v-text-field>
              <v-text-field
                v-model="homepage"
                label="Homepage"
                prepend-icon="mdi-home"
                type="text"
                :rules="urlRules"
              ></v-text-field>
              <v-textarea
                v-model="text"
                label="Text"
                name="login"
                prepend-icon="mdi-text"
                type="text"
                :rules="messageRules"
              ></v-textarea>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn
              :disabled="saving"
              :loading="saving"
              color="success"
              @click="saveMessage"
            >Добавить</v-btn>
            <v-btn :disabled="saving" color="secondary" @click="clearForm">Очистить</v-btn>
            <v-spacer></v-spacer>
            <v-btn :disabled="saving" :to="backPath" color="primary">Отмена</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "CreateMessage",

  props: ["pageNumber", "sortColumn", "descendingOrder"],

  data() {
    return {
      saving: false,

      // Validation
      userNameRules: [
        (v) => !!v || "User Name обязателен для заполнения",
        (v) =>
          (v.length >= 5 && v.length <= 20) ||
          "User Name должен содержать от 5 до 20 символов",
        (v) =>
          this.validateUserName(v) ||
          "User Name должен содержать цифры и буквы латинского алфавита",
      ],
      emailRules: [
        (v) => !!v || "Email обязателен для заполнения",
        (v) =>
          this.validateEmail(v) ||
          "Email не соответствует формату электронной почты",
      ],
      urlRules: [
        (v) =>
          !v || v.length === 0 || this.validateURL(v) || "Url введен неверно",
      ],
      messageRules: [(v) => !!v || "Text обязателен для заполнения"],

      // Form input data
      userName: "",
      email: "",
      homepage: "",
      text: "",
    };
  },
  computed: {
    backPath() {
      return (
        "/messages/" +
        this.pageNumber +
        "," +
        this.sortColumn +
        "," +
        this.descendingOrder
      );
    },
  },
  methods: {
    // Validation
    validateUserName(userName) {
      const regexLat = /\d+([a-z])+|([a-z])+\d+|([a-z])+/gi;
      const regexOther = /[^a-z\d]+/gi;
      return (
        regexLat.exec(userName) !== null && regexOther.exec(userName) === null
      );
    },
    validateEmail(email) {
      const regex = /^[a-z0-9_.+-]+@[a-z0-9-]+\.[a-z0-9-.]+$/gi;
      return regex.exec(email) !== null;
    },
    validateURL(url) {
      try {
        new URL(url).toString();
        return true;
      } catch (e) {
        return false;
      }
    },

    // ДОБАВИТЬ
    saveMessage() {
      this.saving = true;
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("messages/saveMessage", {
            UserName: this.userName,
            Email: this.email,
            Homepage: this.homepage,
            Text: encodeURI(this.text),
          })
          .then(() => {
            this.$router.push(this.backPath);
          })
          .catch(() => {
            this.saving = false;
          });
      }
    },
    // ОЧИСТИТЬ
    clearForm() {
      this.$refs.form.reset();
    },
  },
};
</script>