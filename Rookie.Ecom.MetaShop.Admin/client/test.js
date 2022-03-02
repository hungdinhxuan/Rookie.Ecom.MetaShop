const a = [1, 2, 3, 4, 5]

const callApi = () => {
    return new Promise((resolve, reject) => {
        resolve(a[Math.floor(Math.random() * 5)] * 100)
    })
}

const handleCallApi = async () => {
    let result = []
    for(var i = 0; i < 10; i++)
    {
        result.push(callApi())
    }
    const res = await Promise.all(result)
    console.log(res)
}


handleCallApi()