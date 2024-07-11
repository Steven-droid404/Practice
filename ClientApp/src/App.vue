<template>
    <div>
        <h1>公司營業收入查詢</h1>
        <input type="file" @change="uploadFile" />
        <input type="text" v-model="companyCode" placeholder="公司代號" />
        <button @click="fetchCompanyRevenue">查詢</button>
        <ul>
            <li v-for="revenue in revenues" :key="revenue.id">
                {{ revenue.CompanyName }} - {{ revenue.Revenue }} - {{ revenue.YearMonth }}
            </li>
        </ul>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                companyCode: '',
                revenues: []
            };
        },
        methods: {
            async fetchCompanyRevenue() {
                try {
                    const response = await axios.get(`/api/revenue/select/${this.companyCode}`);
                    this.revenues = response.data;
                } catch (error) {
                    alert('查詢發生錯誤: ' + error);
                }
            },
            async uploadFile(event) {
                const file = event.target.files[0];
                const formData = new FormData();
                formData.append('file', file);

                try {
                    await axios.post('/api/revenue/insert', formData, {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    });
                    alert('上傳成功');
                } catch (error) {
                    alert('上傳發生錯誤: ' + error);
                }
            }
        }
    };
</script>
