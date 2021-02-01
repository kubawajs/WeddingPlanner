import Head from 'next/head'
import Link from 'next/link'
import Layout from '../../components/layout'

export default function WeddingHalls() {
    return (
        <Layout>
            <Head>
                <title>Wedding Planner - sale weselne</title>
            </Head>
            <h1>Sale weselne</h1>
            <h2>
                <Link href="/">Powrót do strony głównej</Link>
            </h2>
        </Layout>
    )
}