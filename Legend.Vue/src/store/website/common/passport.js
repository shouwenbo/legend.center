import fetch from '@/api/fetch'

const state = {
  passportInfo: null
}

const getters = {
  getPassportInfo: state => state.passportInfo
}

const mutations = {
  setPassportInfo: (state, info) => {
    state.passportInfo = info
  }
}

const actions = {
  queryPassportInfo: async function ({
    commit
  }) {
    fetch({
      url: `/api/order/searchStafferPaymentList`,
      method: 'post',
      data: { ...state.filter }
    }).then(ret => {
      console.log(ret)
    })
  }
}
