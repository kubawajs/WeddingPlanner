import Head from 'next/head'
import Link from 'next/link'
import Layout from '../../components/layout'

export default function Guests() {
    return (
        <Layout>
            <Head>
                <title>Wedding Planner - lista gości</title>
            </Head>
            <h1>Lista gości</h1>
            <h2>
                <Link href="/">Powrót do strony głównej</Link>
            </h2>
        </Layout>
    )
}