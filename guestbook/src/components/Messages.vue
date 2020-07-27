<template>
  <v-card dark flat class="fill-height" tile>
    <v-toolbar ref="msgListToolbar" color="primary" dense flat>
      <v-toolbar-title>Список сообщений</v-toolbar-title>
      <v-spacer />
      <v-btn :icon="isXS" :to="createMessagePath" text>
        <v-icon :left="!isXS">mdi-pen</v-icon>
        {{isXS ? '' : 'Добавить сообщение'}}
      </v-btn>
    </v-toolbar>
    <v-data-table
      :height="tableHeight"
      :headers="headers"
      :items="messageList"
      :server-items-length="rowsCount"
      :options.sync="options"
      :loading="loading"
      hide-default-footer
      dense
    >
      <template
        v-slot:item.index="{ item }"
      >{{(page - 1) * rowsPerPage + messageList.indexOf(item) + 1}}</template>
      <template v-slot:item.dateC="{ item }">{{localeDateTimeString(item.dateC)}}</template>
      <template v-slot:item.text="{ item }">{{decodeURI(item.text)}}</template>
    </v-data-table>
    <v-pagination :value="page" :length="pageCount" @input="pageClicked($event)" />
  </v-card>
</template>

<script>
export default {
  name: "Messages",

  props: ["pageNumber", "sortColumn", "descendingOrder"],

  data() {
    return {
      // Element resizing
      toolBarHeight: undefined,
      windowHeight: 0,

      page: 1,
      headers: [
        {
          text: "",
          align: "center",
          sortable: false,
          value: "index",
          divider: true,
        },
        {
          text: "Дата добавления",
          align: "center",
          sortable: true,
          value: "dateC",
          divider: true,
        },
        {
          text: "Имя пользователя",
          align: "start",
          sortable: true,
          value: "userName",
          divider: true,
        },
        {
          text: "E-mail",
          align: "start",
          sortable: true,
          value: "email",
          divider: true,
        },
        {
          text: "Текст",
          align: "start",
          sortable: false,
          value: "text",
          divider: true,
        },
      ],
      messageList: [],
      rowsCount: 0,
      rowsPerPage: 25,
      options: { mustSort: true },
      loading: false,
    };
  },
  computed: {
    tableHeight() {
      if (this.toolBarHeight !== undefined)
        return this.windowHeight - this.toolBarHeight - 59;
      return undefined;
    },
    isXS() {
      return this.$vuetify.breakpoint.xs;
    },

    createMessagePath() {
      return (
        "/create-message/" +
        this.paging(this.pageNumber, this.sortColumn, this.descendingOrder)
      );
    },

    pageCount() {
      if (this.rowsCount > 0)
        return Math.ceil(this.rowsCount / this.rowsPerPage);
      else return 1;
    },

    isDescendingOrder() {
      return this.descendingOrder == "true"
        ? true
        : this.descendingOrder == "false"
        ? false
        : undefined;
    },
  },
  methods: {
    handleWindowResize() {
      this.windowHeight = window.innerHeight;
    },
    paging(pageNumber, sortColumn, descendingOrder) {
      return pageNumber + "&" + sortColumn + "&" + descendingOrder;
    },

    localeDateTimeString(date) {
      return new Date(date).toLocaleString();
    },

    loadMessageList() {
      let options = {
        PageNumber: Number(this.pageNumber),
        PageSize: this.rowsPerPage,
        SortColumn: this.sortColumn,
        DescendingOrder: this.isDescendingOrder,
      };

      this.loading = true;
      this.$store
        .dispatch("messages/getMessages", options)
        .then((responce) => {
          this.loading = false;
          this.messageList = responce.data.messageList;
          this.rowsCount = responce.data.rowsCount;
          this.options.page = options.PageNumber;
          this.page = options.PageNumber;
          this.options.sortBy = [options.SortColumn];
          this.options.sortDesc = [options.DescendingOrder];
        })
        .catch(() => {
          this.loading = false;
        });
    },

    pageClicked(page) {
      if (this.page !== page) {
        this.$router
          .push(
            "/messages/" +
              this.paging(page, this.sortColumn, this.descendingOrder)
          )
          .then(() => {
            this.loadMessageList();
          })
          .catch(() => {});
      }
    },
  },
  watch: {
    options: function () {
      if (
        this.options.sortBy[0] &&
        this.options.sortDesc[0] !== undefined &&
        (this.options.sortBy[0] !== this.sortColumn ||
          this.options.sortDesc[0] !== this.isDescendingOrder)
      ) {
        this.$router
          .push(
            "/messages/" +
              this.paging(1, this.options.sortBy[0], this.options.sortDesc[0])
          )
          .then(() => {
            this.loadMessageList();
          })
          .catch(() => {});
      }
    },
  },
  destroyed() {
    window.removeEventListener("resize", this.handleWindowResize);
  },
  mounted() {
    window.addEventListener("resize", this.handleWindowResize);
    this.handleWindowResize();
    this.toolBarHeight = this.$refs.msgListToolbar.computedContentHeight;

    if (
      !Number.isSafeInteger(Number(this.pageNumber)) ||
      !this.headers.find((item) => item.value === this.sortColumn) ||
      this.isDescendingOrder === undefined
    ) {
      this.$router
        .push("/messages/" + this.paging(1, "userName", false))
        .then(() => {
          this.loadMessageList();
        })
        .catch(() => {});
    } else this.loadMessageList();
  },
};
</script>