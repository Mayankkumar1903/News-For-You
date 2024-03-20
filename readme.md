# News for You (News Feed Application)

## Introduction

News for You is a sophisticated web application designed to aggregate and categorize news from leading news agencies, tailored to user preferences. The application integrates RSS feeds from various news agencies, categorizes the news, and allows users to filter news by category. It also provides a feature to track the number of clicks on each news item, offering insights into user engagement.

## Features

### User Authentication

- **Initialization**: Set up the initial data for the application, including categories, agencies, and agency feed data.
- **News Fetching**: Retrieve and store news references from specified RSS feeds into the local database.

### News Display

- **Category Filtering**: Allow users to select one or multiple categories to filter the news feed. The user's category selection is preserved across browser sessions.
- **News Presentation**: Display news with the latest items first. The list auto-refreshes or notifies users when new news is added.

### Admin Panel

- **Add News Agencies and News Categories** : Allow admin to add or configure new category of news or add a new agency that provides news feeds.

### Reports

- **News Click Count Report**: Export to PDF , the number of clicks for each news item, categorized by agency , category and title ( for admins only to analyze the news feeds ).

## Entities / Attributes

- **User**:
- user_id
- name
- email (used for sign-in)
- password
- **Category**:
- category_id
- category_title
- **Agency**:
- agency_id
- agency_name
- agency_logo_path
- **Agency Feed**:
- agency_feed_id
- agency_feed_url
- agency_id
- category_id
- **News**:
- news_id
- news_title
- news_description
- news_publish_date_time
- news_link (unique)
- click_count
- category_id
- agency_id
