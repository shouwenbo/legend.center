import axios from 'axios'

export default function fetch (options) {
  return new Promise((resolve, reject) => {
    const instance = axios.create({
      baseURL: `127.0.0.1:8000`,
      timeout: 6000,
      headers: {
        'Content-Type': 'application/json; charset=utf-8'
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
        console.log(err, 'instance.interceptors.response.use.err')
        return Promise.reject(err)
      })
    instance(options)
      .then((res) => {
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
