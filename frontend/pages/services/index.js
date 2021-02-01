import Head from 'next/head'
import Link from 'next/link'
import Layout from '../../components/layout'

export default function Services() {
    return (
        <Layout>
            <Head>
                <title>Wedding Planner - usługi</title>
            </Head>
            <h1>Usługi ślubne</h1>
            <h2>
                <Link href="/">Powrót do strony głównej</Link>
            </h2>
        </Layout>
    )
}