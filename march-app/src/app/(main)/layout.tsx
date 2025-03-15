import MainLayoutComponent from "@/components/layout/main.layout.component";

export default async function MainLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <>
      <MainLayoutComponent>
        {children}
      </MainLayoutComponent>
    </>
  )
}