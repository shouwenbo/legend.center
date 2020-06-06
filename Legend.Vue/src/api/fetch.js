// import axios from 'axios'

// axios.defaults.headers.post['Content-Type'] = 'application/json;charset=UTF-8'

// class FetchHttp {
//   static async fetchData (url, reqType, options, otherType) {
//     let config = {
//       credentials: 'include',
//       mode: 'cors',
//       timeout: 1000 * 60 * 2
//     }
//     if (otherType === 'file') {
//       config = {
//         ...config,
//         ...{
//           headers: {
//             'Content-Type': 'multipart/form-data'
//           }
//         }
//       }
//     } else {
//       config = {
//         ...config,
//         ...{
//           headers: {
//             'Accept': 'application/json, text/plain, */*',
//             'Content-Type': 'application/json;charset=UTF-8',
//             'X-Requested-With': 'XMLHttpRequest'
//           }
//         }
//       }
//     }
//     let data = {}
//     if (reqType === 'post') {
//       if (otherType === 'file') {
//         var param = new FormData()
//         param.append('file', options.file)
//         data = param
//       } else {
//         data = JSON.stringify(options)
//       }
//     } else {
//       data = {}
//     }
//     let response = {}
//     try {
//       await axios[reqType](url, data, config).then(r => {
//         response = r.data || {}
//       }).catch((e, vv, ccc) => {
//         if (e.response) {
//           if (e.response.status === 401) {
//           } else {
//           }
//         }
//         response = e.response
//         return Promise.reject(e.response)
//       })
//     } catch (error) { // 这里 undefined
//       console.log('catch error', error)
//     }
//     if (response && response.error && response.error.message) {
//     }
//     return response
//   }
//   static get (url, options) {
//     const fetchUrl = this.serializeParme(url, options)
//     return this.fetchData(fetchUrl, 'get', {})
//   }
//   static post (url, options, otherType) {
//     return this.fetchData(url, 'post', options, otherType)
//   }
//   static serializeParme (url, options) {
//     let urltmp = url
//     if (options) {
//       const paramsArray = []
//       Object.keys(options).forEach(key => paramsArray.push(key + '=' + options[key]))
//       if (url.search(/\?/) === -1) {
//         urltmp += '?' + paramsArray.join('&')
//       } else {
//         urltmp += '&' + paramsArray.join('&')
//       }
//     }
//     return urltmp
//   }
// }

// export default FetchHttp

import axios from 'axios'
import router from '@/router'

export default function fetch (options) {
  return new Promise((resolve, reject) => {
    const instance = axios.create({
      baseURL: `http://172.20.70.152:53882`,
      timeout: 6000,
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJleHAiOjE1OTE1MjgwNTUsImlzcyI6Ind3dy5iYWlkdS5jb20iLCJhdWQiOiJ3d3cuYmFpZHUuY29tIn0.tvqHhTWazwJbL0TFOq5BlBME2liNSdXdexBJ6tZC-FM'
      }
    })
    instance.interceptors.request.use(
      config => {
        console.log(config, 'instance.interceptors.request.use.config')
        return config
      },
      err => {
        console.log(err, 'instance.interceptors.request.use.err')
        return Promise.reject(err)
      })
    instance.interceptors.response.use(
      response => {
        console.log(response, 'instance.interceptors.response.use.response')
        return response
      },
      err => {
        console.log(err, 'err?')
        if (err.response.status === 401) {
          router.push({ path: '/PassportLogin' })
        }
        console.log(err.response, 'instance.interceptors.response.use.err')
        return Promise.reject(err)
      })
    instance(options)
      .then((res) => {
        console.log(res)
        let { code } = res.data
        if (code === 401) {
          // router.push({ name: 'login' })
        }
        if (code === 400) {
          //
        }
        resolve(res.data)
      })
      .catch((error) => {
        reject(error)
      })
  })
}
